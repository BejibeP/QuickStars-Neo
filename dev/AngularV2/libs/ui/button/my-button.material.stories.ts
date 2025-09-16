import { Meta, Story } from '@storybook/angular';
import { moduleMetadata } from '@storybook/angular';
import { MyButtonComponent } from './my-button.component';
import { MatButtonModule } from '@angular/material/button';

export default {
  title: 'UI/MyButton (Material)',
  component: MyButtonComponent,
  decorators: [
    moduleMetadata({
      imports: [MatButtonModule],
    })
  ]
} as Meta;

const Template: Story<MyButtonComponent> = (args) => ({
  props: args,
  template: '<button mat-button>{{label || "Material Button"}}</button>'
});

export const MaterialPrimary = Template.bind({});
MaterialPrimary.args = { label: 'Material Click' };
