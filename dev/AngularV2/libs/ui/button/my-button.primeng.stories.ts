import { Meta, Story } from '@storybook/angular';
import { moduleMetadata } from '@storybook/angular';
import { MyButtonComponent } from './my-button.component';
import { ButtonModule } from 'primeng/button';

export default {
  title: 'UI/MyButton (PrimeNG)',
  component: MyButtonComponent,
  decorators: [
    moduleMetadata({
      imports: [ButtonModule],
    })
  ]
} as Meta;

const Template: Story<MyButtonComponent> = (args) => ({
  props: args,
  template: '<button pButton type="button" label="PrimeNG Button"></button>'
});

export const PrimePrimary = Template.bind({});
PrimePrimary.args = {};
