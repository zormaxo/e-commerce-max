import { Directive, ElementRef, HostListener, OnInit, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appOnlyNumber]',
})
export default class OnlyNumberDirective implements OnInit {
  private regex = new RegExp(/^\d*$/);

  // Allow key codes for special events
  private specialKeys: Array<string> = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight'];

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnInit(): void {
    this.renderer.addClass(this.el.nativeElement, 'px-1');
    this.renderer.addClass(this.el.nativeElement, 'form-control-sm');
  }

  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    // Allow Backspace, tab, end, and home keys
    if (this.specialKeys.indexOf(event.key) !== -1) {
      return;
    }

    // Do not use event.keycode this is deprecated. See: https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/keyCode
    const current: string = this.el.nativeElement.value.replaceAll('.', '');
    // We need this because the current value on the DOM element is not yet updated with the value from this event
    const next: string = current.concat(event.key);
    if (String(next).match(this.regex)) {
      this.el.nativeElement.value = new Intl.NumberFormat('tr-TR').format(parseInt(next));
    }
    event.preventDefault();
  }
}
