export interface Category {
  id: string;
  name: string;
  taskCount: number;
}

export interface CategoryCreateRequest {
  name: string;
}
