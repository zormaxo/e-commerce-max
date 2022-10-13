import { Directive, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appSort]',
})
export class SortDirective implements OnInit {
  @Input('appSort') sortTextInput: string;
  @Output() headerClicked: EventEmitter<string> = new EventEmitter();

  arrow: Direction = Direction.None;
  down: unknown;
  up: unknown;
  space: { innerText: string };
  thText: ChildNode;

  constructor(private elRef: ElementRef, private renderer: Renderer2) {
    this.CreateElements();
  }
  ngOnInit(): void {
    this.thText = (<HTMLElement>this.elRef.nativeElement).firstChild;
    this.renderer.addClass(this.thText, 'text-primary');

    this.renderer.listen(this.thText, 'mouseenter', (event) => {
      this.renderer.addClass(this.thText, 'text-decoration-underline');
    });

    this.renderer.listen(this.thText, 'mouseleave', (event) => {
      this.renderer.removeClass(this.thText, 'text-decoration-underline');
    });

    this.renderer.setStyle(this.thText, 'cursor', 'pointer');
  }

  @HostListener('click') click() {
    const tableElement: HTMLTableElement = this.elRef.nativeElement.parentNode.parentNode.parentNode;
    const elements = tableElement.getElementsByClassName('sortem');
    Array.from(elements).forEach((el) => {
      this.renderer.removeChild(el.parentNode, el);
    });

    let sortText: string;
    if (this.arrow == Direction.None) {
      this.renderer.appendChild(this.elRef.nativeElement, this.space);
      this.renderer.appendChild(this.elRef.nativeElement, this.up);
      this.arrow = Direction.Asc;
      sortText = this.sortTextInput + ' ' + Direction.Asc;
    } else if (this.arrow == Direction.Asc) {
      this.renderer.appendChild(this.elRef.nativeElement, this.down);
      this.arrow = Direction.Desc;
      sortText = this.sortTextInput + ' ' + Direction.Desc;
    } else if (this.arrow == Direction.Desc) {
      this.renderer.appendChild(this.elRef.nativeElement, this.up);
      this.arrow = Direction.Asc;
      sortText = this.sortTextInput + ' ' + Direction.Asc;
    }
    this.headerClicked.emit(sortText);
  }

  private CreateElements() {
    this.space = this.renderer.createElement('span');
    this.space.innerText = ' ';

    this.down = this.renderer.createElement('em');
    this.renderer.addClass(this.down, 'fa-solid');
    this.renderer.addClass(this.down, 'fa-caret-down');
    this.renderer.addClass(this.down, 'sortem');

    this.up = this.renderer.createElement('em');
    this.renderer.addClass(this.up, 'fa-solid');
    this.renderer.addClass(this.up, 'fa-caret-up');
    this.renderer.addClass(this.up, 'sortem');
  }
}

enum Direction {
  Asc = 'asc',
  Desc = 'desc',
  None = '',
}
