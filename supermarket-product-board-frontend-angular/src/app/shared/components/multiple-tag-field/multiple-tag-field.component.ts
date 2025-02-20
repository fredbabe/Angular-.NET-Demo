import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  forwardRef,
  Input,
  Output,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'multiple-tag-field',
  imports: [CommonModule],
  templateUrl: './multiple-tag-field.component.html',
  styleUrl: './multiple-tag-field.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MultipleTagFieldComponent),
      multi: true,
    },
  ],
})
export class MultipleTagFieldComponent implements ControlValueAccessor {
  @Input() label: string = 'Select value';
  @Input() isDisabled: boolean = false;

  @Output() valueChange = new EventEmitter<string[]>();

  values: string[] = [];

  // Control value accessor functions
  private onChange: (value: any) => void = () => {};
  private onTouched: () => void = () => {};

  writeValue(obj: any): void {
    this.values = obj;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // TODO
  }

  // Functions
  addElement(value: string) {
    this.values.push(value);
    this.valueChange.emit(this.values);
    this.onChange(this.values);
  }

  removeElement(index: number) {
    this.values.splice(index, 1);
    this.valueChange.emit(this.values);
    this.onChange(this.values);
  }
}
