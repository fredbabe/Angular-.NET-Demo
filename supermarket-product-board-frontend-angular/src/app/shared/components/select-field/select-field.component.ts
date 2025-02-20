import {
  Component,
  ElementRef,
  EventEmitter,
  forwardRef,
  Input,
  Output,
  signal,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectOption } from '../../../models/select-option';
import { IconArrowDownComponent } from '../../icons/icon-arrow-down/icon-arrow-down.component';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'select-field',
  imports: [CommonModule, IconArrowDownComponent],
  templateUrl: './select-field.component.html',
  styleUrl: './select-field.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SelectFieldComponent),
      multi: true,
    },
  ],
})
export class SelectFieldComponent implements ControlValueAccessor {
  @Input() label: string = 'Select value';
  @Input() subLabel: string = 'Select value';
  @Input() errorMessage: string = '';
  @Input() showError?: boolean = false;
  @Input() options: SelectOption[] = [];
  @Input() isDisabled: boolean = false;

  @Output() valueChange = new EventEmitter<string>();

  viewValues = signal(false);

  // Test
  selectedValue: string | null = null;

  // Control value accessor functions
  private onChange: (value: any) => void = () => {};
  private onTouched: () => void = () => {};

  constructor(private elementRef: ElementRef) {}

  writeValue(obj: string): void {
    this.selectedValue = obj;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // Handle disable state if needed
  }

  toggleDropdown() {
    this.viewValues.update((prev) => !prev);
  }

  handleSelect(option: SelectOption) {
    this.label = option.name ?? 'N/A';
    this.viewValues.update(() => false);
    this.valueChange.emit(option.value);
    this.onChange(option.value); // Notify the form that the value changed
  }

  ngAfterViewInit() {
    document.addEventListener('click', this.handleClickOutside.bind(this));
  }

  ngOnDestroy() {
    document.removeEventListener('click', this.handleClickOutside.bind(this));
  }

  handleClickOutside(event: MouseEvent) {
    const clickedElement = event.target as HTMLElement;
    if (!this.elementRef.nativeElement.contains(clickedElement)) {
      this.viewValues.set(false);
    }
  }
}
