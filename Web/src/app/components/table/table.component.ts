import { Component } from '@angular/core';
import { CategoryBadgeComponent } from '../category-badge/category-badge.component';
import { Task } from '../../models/task';
import { Store } from '@ngrx/store';
import { getTasksList } from '../../store/task.selector';
import { AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [CategoryBadgeComponent, AsyncPipe],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent {
  
  tasks$: Observable<ReadonlyArray<Task>> = this.store.select(getTasksList);

  constructor(private store: Store) {}
}
