import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../../components/table/table.component';
import { FiltersComponent } from '../../components/filters/filters.component';
import { Store } from '@ngrx/store';
import { loadTasks } from '../../store/task.actions';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ TableComponent, FiltersComponent ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit{

  constructor(private store: Store) {}
  
  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.store.dispatch(loadTasks({ category: 'all', status: 'all' }));
  }
}
