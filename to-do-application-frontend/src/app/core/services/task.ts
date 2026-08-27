import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PagedResult } from '../models/paged-result.model';
import {
  TaskCreateRequest,
  TaskItem,
  TaskQueryParams,
  TaskUpdateRequest,
} from '../models/task.model';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private apiUrl = `${environment.apiUrl}/tasks`;

  constructor(private http: HttpClient) {}

  getFiltered(query: TaskQueryParams): Observable<PagedResult<TaskItem>> {
    let params = new HttpParams();

    if (query.searchTerm) params = params.set('searchTerm', query.searchTerm);
    if (query.categoryId) params = params.set('categoryId', query.categoryId);
    if (query.isCompleted !== undefined) params = params.set('isCompleted', query.isCompleted);
    if (query.pageNumber) params = params.set('pageNumber', query.pageNumber);
    if (query.pageSize) params = params.set('pageSize', query.pageSize);

    return this.http.get<PagedResult<TaskItem>>(this.apiUrl, { params });
  }

  getById(id: string): Observable<TaskItem> {
    return this.http.get<TaskItem>(`${this.apiUrl}/${id}`);
  }

  create(data: TaskCreateRequest): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.apiUrl, data);
  }

  update(id: string, data: TaskUpdateRequest): Observable<TaskItem> {
    return this.http.put<TaskItem>(`${this.apiUrl}/${id}`, data);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
