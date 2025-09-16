import { Component, Input } from '@angular/core';

@Component({
  selector: 'my-button',
  template: `<button type="button"><ng-content></ng-content>{{label ? ' ' + label : ''}}</button>`
})
export class MyButtonComponent {
  @Input() label?: string;
}
