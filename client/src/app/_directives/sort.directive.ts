import { Directive, ElementRef, EventEmitter, HostListener, OnInit, Output, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appSort]',
})
export class SortDirective implements OnInit {
  @Output() someEvent: EventEmitter<string> = new EventEmitter();
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
  }

  @HostListener('click') click() {
    const tableElement: HTMLTableElement = this.elRef.nativeElement.parentNode.parentNode.parentNode;
    const elements = tableElement.getElementsByClassName('sortem');
    Array.from(elements).forEach((el) => {
      this.renderer.removeChild(el.parentNode, el);
    });

    if (this.arrow == Direction.None) {
      this.renderer.appendChild(this.elRef.nativeElement, this.space);
      this.renderer.appendChild(this.elRef.nativeElement, this.down);
      this.arrow = Direction.Down;
    } else if (this.arrow == Direction.Down) {
      this.renderer.appendChild(this.elRef.nativeElement, this.up);
      this.arrow = Direction.Up;
    } else if (this.arrow == Direction.Up) {
      this.renderer.appendChild(this.elRef.nativeElement, this.down);
      this.arrow = Direction.Down;
    }

    this.someEvent.emit('omer');
  }

  @HostListener('mouseenter') mouseenter() {
    this.renderer.addClass(this.thText, 'text-decoration-underline');
    this.renderer.setStyle(this.thText, 'cursor', 'pointer');
  }

  @HostListener('mouseleave') mouseleave() {
    this.renderer.removeClass(this.thText, 'text-decoration-underline');
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
  Up = 1,
  Down,
  None,
}
