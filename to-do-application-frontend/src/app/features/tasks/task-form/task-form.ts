import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TaskService } from '../../../core/services/task';
import { TaskItem } from '../../../core/models/task.model';
import { Category } from '../../../core/models/category.model';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-form.html',
  styleUrl: './task-form.scss',
})
export class TaskFormComponent implements OnChanges {
  @Input() task: TaskItem | null = null;
  @Input() categories: Category[] = [];

  @Output() saved = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  form: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
  ) {
    this.form = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      description: [''],
      priority: ['Medium', Validators.required],
      dueDate: [''],
      categoryId: [''],
    });
  }

  ngOnChanges(): void {
    if (this.task) {
      this.form.patchValue({
        title: this.task.title,
        description: this.task.description ?? '',
        priority: this.task.priority,
        dueDate: this.task.dueDate ?? '',
        categoryId: this.task.categoryId ?? '',
      });
    } else {
      this.form.reset({ priority: 'Medium' });
    }
  }

  get isEditMode(): boolean {
    return !!this.task;
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const raw = this.form.value;
    const payload = {
      title: raw.title,
      description: raw.description || undefined,
      priority: raw.priority,
      dueDate: raw.dueDate || undefined,
      categoryId: raw.categoryId || undefined,
    };

    const request = this.isEditMode
      ? this.taskService.update(this.task!.id, { ...payload, isCompleted: this.task!.isCompleted })
      : this.taskService.create(payload);

    request.subscribe({
      next: () => this.saved.emit(),
      error: (err) => (this.errorMessage = err.error?.message || 'Помилка збереження'),
    });
  }

  onCancel(): void {
    this.cancelled.emit();
  }
}
