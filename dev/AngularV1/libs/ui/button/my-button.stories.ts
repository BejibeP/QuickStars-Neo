import { Meta, Story } from '@storybook/angular';
import { MyButtonComponent } from './my-button.component';

export default {
  title: 'UI/MyButton',
  component: MyButtonComponent,
} as Meta;

const Template: Story<MyButtonComponent> = (args) => ({
  props: args,
  template: '<my-button [label]="label">Inner</my-button>'
});

export const Primary = Template.bind({});
Primary.args = { label: 'Click me' };
