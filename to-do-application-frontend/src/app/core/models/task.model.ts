export type TaskPriority = 'Low' | 'Medium' | 'High';

export interface TaskItem {
  id: string;
  title: string;
  description: string | null;
  isCompleted: boolean;
  priority: TaskPriority;
  dueDate: string | null;
  createdAt: string;
  categoryId: string | null;
  categoryName: string | null;
}

export interface TaskCreateRequest {
  title: string;
  description?: string;
  priority: TaskPriority;
  dueDate?: string;
  categoryId?: string;
}

export interface TaskUpdateRequest {
  title: string;
  description?: string;
  isCompleted: boolean;
  priority: TaskPriority;
  dueDate?: string;
  categoryId?: string;
}

export interface TaskQueryParams {
  searchTerm?: string;
  categoryId?: string;
  isCompleted?: boolean;
  pageNumber?: number;
  pageSize?: number;
}
