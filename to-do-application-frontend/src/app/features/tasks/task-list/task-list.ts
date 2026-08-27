import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, switchMap } from 'rxjs';
import { TaskService } from '../../../core/services/task';
import { CategoryService } from '../../../core/services/category';
import { AuthService } from '../../../core/services/auth';
import { TaskItem, TaskQueryParams } from '../../../core/models/task.model';
import { Category } from '../../../core/models/category.model';
import { TaskItemComponent } from '../task-item/task-item';
import { TaskFormComponent } from '../task-form/task-form';
import { PaginationComponent } from '../../../shared/pagination/pagination';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule, TaskItemComponent, TaskFormComponent, PaginationComponent],
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss',
})
export class TaskListComponent implements OnInit, OnDestroy {
  tasks: TaskItem[] = [];
  categories: Category[] = [];

  totalCount = 0;
  totalPages = 0;

  query: TaskQueryParams = {
    pageNumber: 1,
    pageSize: 5,
  };

  isLoading = false;
  showForm = false;
  editingTask: TaskItem | null = null;

  private reload$ = new Subject<void>();

  constructor(
    private taskService: TaskService,
    private categoryService: CategoryService,
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.loadCategories();

    this.reload$
      .pipe(
        switchMap(() => {
          this.isLoading = true;
          return this.taskService.getFiltered(this.query);
        }),
      )
      .subscribe({
        next: (result) => {
          this.tasks = result.items;
          this.totalCount = result.totalCount;
          this.totalPages = result.totalPages;
          this.isLoading = false;
          this.cdr.detectChanges();
        },
        error: () => {
          this.isLoading = false;
          this.cdr.detectChanges();
        },
      });

    this.loadTasks();
  }

  ngOnDestroy(): void {
    this.reload$.complete();
  }

  loadCategories(): void {
    this.categoryService.getAll().subscribe({
      next: (data) => {
        this.categories = data;
        this.cdr.detectChanges();
      },
    });
  }

  loadTasks(): void {
    this.reload$.next();
  }

  onSearchChange(term: string): void {
    this.query.searchTerm = term || undefined;
    this.query.pageNumber = 1;
    this.loadTasks();
  }

  onCategoryFilterChange(categoryId: string): void {
    this.query.categoryId = categoryId || undefined;
    this.query.pageNumber = 1;
    this.loadTasks();
  }

  onCompletedFilterChange(value: string): void {
    this.query.isCompleted = value === '' ? undefined : value === 'true';
    this.query.pageNumber = 1;
    this.loadTasks();
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.query.pageNumber = page;
    this.loadTasks();
  }

  openCreateForm(): void {
    this.editingTask = null;
    this.showForm = true;
  }

  openEditForm(task: TaskItem): void {
    this.editingTask = task;
    this.showForm = true;
  }

  closeForm(): void {
    this.showForm = false;
    this.editingTask = null;
  }

  onFormSaved(): void {
    this.closeForm();
    this.loadTasks();
  }

  onToggleCompleted(task: TaskItem): void {
    this.taskService
      .update(task.id, {
        title: task.title,
        description: task.description ?? undefined,
        isCompleted: !task.isCompleted,
        priority: task.priority,
        dueDate: task.dueDate ?? undefined,
        categoryId: task.categoryId ?? undefined,
      })
      .subscribe({
        next: () => this.loadTasks(),
      });
  }

  onDeleteTask(taskId: string): void {
    if (!confirm('Видалити цю задачу?')) return;

    this.taskService.delete(taskId).subscribe({
      next: () => this.loadTasks(),
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  goToCategories(): void {
    this.router.navigate(['/categories']);
  }
}
