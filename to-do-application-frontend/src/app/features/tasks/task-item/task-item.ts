import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskItem } from '../../../core/models/task.model';

@Component({
  selector: 'app-task-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-item.html',
  styleUrl: './task-item.scss',
})
export class TaskItemComponent {
  @Input({ required: true }) task!: TaskItem;

  @Output() edit = new EventEmitter<TaskItem>();
  @Output() delete = new EventEmitter<string>();
  @Output() toggleCompleted = new EventEmitter<TaskItem>();

  onEdit(): void {
    this.edit.emit(this.task);
  }

  onDelete(): void {
    this.delete.emit(this.task.id);
  }

  onToggle(): void {
    this.toggleCompleted.emit(this.task);
  }

  get priorityClass(): string {
    switch (this.task.priority) {
      case 'High':
        return 'text-danger';
      case 'Medium':
        return 'text-warning';
      case 'Low':
        return 'text-success';
      default:
        return '';
    }
  }
}
