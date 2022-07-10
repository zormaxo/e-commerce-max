import { Directive, ElementRef, EventEmitter, HostListener, Output, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appSort]',
})
export class SortDirective {
  @Output() someEvent: EventEmitter<string> = new EventEmitter();
  arrow = 'none';
  down: unknown;
  up: unknown;
  space: { innerText: string; };

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
  }

  @HostListener('click') click() {
    if (this.arrow == 'none') {
      this.renderer.appendChild(this.elRef.nativeElement, this.space);
      this.renderer.appendChild(this.elRef.nativeElement, this.down);
      this.arrow = 'down';
    } else if (this.arrow == 'down') {
      this.renderer.removeChild(this.elRef.nativeElement, this.down);
      this.renderer.appendChild(this.elRef.nativeElement, this.up);
      this.arrow = 'up';
    } else if (this.arrow == 'up') {
      this.renderer.removeChild(this.elRef.nativeElement, this.up);
      this.renderer.appendChild(this.elRef.nativeElement, this.down);
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
