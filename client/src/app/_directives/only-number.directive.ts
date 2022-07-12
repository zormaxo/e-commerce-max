import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appOnlyNumber]',
})
export class OnlyNumberDirective {
  private regex = new RegExp(/^\d*$/);

  // Allow key codes for special events
  private specialKeys: Array<string> = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight'];

  constructor(private el: ElementRef) {}

  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    // Allow Backspace, tab, end, and home keys
    if (this.specialKeys.indexOf(event.key) !== -1) {
      return;
    }

    // Do not use event.keycode this is deprecated. See: https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/keyCode
    const current: string = this.el.nativeElement.value;
    // We need this because the current value on the DOM element is not yet updated with the value from this event
    const next: string = current.concat(event.key);
    if (next && !String(next).match(this.regex)) {
      event.preventDefault();
    } else {
      let omer = parseInt(next).toLocaleString();
      this.el.nativeElement.value = omer;
      event.preventDefault();
    }
  }
}
