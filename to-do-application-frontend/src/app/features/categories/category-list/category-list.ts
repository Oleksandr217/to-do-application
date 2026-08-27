import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from '../../../core/services/category';
import { Category } from '../../../core/models/category.model';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category-list.html',
  styleUrl: './category-list.scss',
})
export class CategoryListComponent implements OnInit {
  categories: Category[] = [];
  form: FormGroup;
  errorMessage: string | null = null;
  editingId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAll().subscribe({
      next: (data) => (this.categories = data),
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const request = this.editingId
      ? this.categoryService.update(this.editingId, this.form.value)
      : this.categoryService.create(this.form.value);

    request.subscribe({
      next: () => {
        this.form.reset();
        this.editingId = null;
        this.errorMessage = null;
        this.loadCategories();
      },
      error: (err) => (this.errorMessage = err.error?.message || 'Помилка збереження'),
    });
  }

  onEdit(category: Category): void {
    this.editingId = category.id;
    this.form.patchValue({ name: category.name });
  }

  onCancelEdit(): void {
    this.editingId = null;
    this.form.reset();
  }

  onDelete(id: string): void {
    if (!confirm("Видалити категорію? Задачі залишаться, але втратять прив'язку.")) return;

    this.categoryService.delete(id).subscribe({
      next: () => this.loadCategories(),
    });
  }

  goBack(): void {
    this.router.navigate(['/tasks']);
  }
}
