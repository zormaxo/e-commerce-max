import { Directive, ElementRef, EventEmitter, HostListener, Output, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appSort]',
})
export class SortDirective {
  @Output() someEvent: EventEmitter<string> = new EventEmitter();
  arrow = 'none';
  down: unknown;
  up: unknown;
  space: { innerText: string };
  omer;
  salih;
  asli;

  constructor(private elRef: ElementRef, private renderer: Renderer2) {
    this.renderer.addClass(this.elRef.nativeElement, 'text-primary');

    this.space = this.renderer.createElement('span');
    this.space.innerText = ' ';

    this.down = this.renderer.createElement('em');
    this.renderer.addClass(this.down, 'fa-solid');
    this.renderer.addClass(this.down, 'fa-caret-down');

    this.up = this.renderer.createElement('em');
    this.renderer.addClass(this.up, 'fa-solid');
    this.renderer.addClass(this.up, 'fa-caret-up');

    this.omer = this.renderer.createElement('span');
    this.omer.innerText = 'omer';

    this.salih = this.renderer.createElement('span');
    this.salih.innerText = 'salih';

    this.asli = this.renderer.createElement('span');
    this.asli.innerText = 'asli';
  }

  @HostListener('click') click() {
    let tableElement = this.elRef.nativeElement.parentNode.parentNode.parentNode;
    if (tableElement instanceof HTMLTableElement) {
      var omer = tableElement.getElementsByTagName('th');
      var salih = omer[2];
      this.renderer.removeChild(salih, this.span);
      this.renderer.removeChild(salih, this.salih);
      this.renderer.removeChild(salih, this.asli);
    }
    if (this.arrow == 'none') {
      this.renderer.appendChild(this.elRef.nativeElement, this.omer);
      // this.renderer.appendChild(this.elRef.nativeElement, this.space);
      // this.renderer.appendChild(this.elRef.nativeElement, this.down);
      this.arrow = 'down';
    } else if (this.arrow == 'down') {
      this.renderer.appendChild(this.elRef.nativeElement, this.salih);
      this.renderer.removeChild(this.elRef.nativeElement, this.asli);
      // this.renderer.removeChild(this.elRef.nativeElement, this.down);
      // this.renderer.appendChild(this.elRef.nativeElement, this.up);
      this.arrow = 'up';
    } else if (this.arrow == 'up') {
      // this.renderer.removeChild(this.elRef.nativeElement, this.up);
      // this.renderer.appendChild(this.elRef.nativeElement, this.down);
      this.renderer.removeChild(this.elRef.nativeElement, this.salih);
      this.renderer.appendChild(this.elRef.nativeElement, this.asli);
      this.arrow = 'down';
    }

    this.someEvent.emit('omer');
  }

  @HostListener('mouseenter') mouseenter() {
    this.renderer.addClass(this.elRef.nativeElement, 'text-decoration-underline');
    this.renderer.setStyle(this.elRef.nativeElement, 'cursor', 'pointer');
  }

  @HostListener('mouseleave') mouseleave() {
    this.renderer.removeClass(this.elRef.nativeElement, 'text-decoration-underline');
  }
}
