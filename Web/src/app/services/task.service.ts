import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { Task } from '../models/task';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TaskService extends BaseService<Task> {
  constructor(http: HttpClient) {
    super(http);
  }

  getData(status: string | null, category: string | null) {
    var url = this.getUrl('api/todos');
    var params = new HttpParams();

    if (status && category) {
      params = params
        .set("status", status)
        .set("category", category);
    }

    return this.http.get<Task[]>(url, { params });
  }

  get(id: string): Observable<Task> {
    var url = this.getUrl('api/todos/' + id);
    return this.http.get<Task>(url);
  }

  put(item: Task): Observable<Task> {
    var url = this.getUrl('api/todos/' + item.id);
    return this.http.put<Task>(url, item);
  }

  post(item: Task): Observable<Task> {
    var url = this.getUrl('api/todos');
    return this.http.post<Task>(url, item);
  }

  delete(id: string): Observable<Task> {
    var url = this.getUrl('api/todos/' + id);
    return this.http.delete<Task>(url);
  }
}
