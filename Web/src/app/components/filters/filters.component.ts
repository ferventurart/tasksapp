import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { loadTasks } from '../../store/task.actions';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent implements OnInit {

  status!: string;
  category!: string;
  public statusList: string[] = ['Pending', 'Cancelled', 'Completed'];
  public categoryList: string[] = ['Orange', 'Yellow', 'Blue'];
  
  constructor(private store: Store) {}
  
  ngOnInit(): void {
    this.status = 'all';
    this.category = 'all';
  }
  
  reloadData()
  {
    this.store.dispatch(loadTasks({ category : this.category, status: this.status }));
  }

  onChangeCategory($event: any)
  {  
    this.category = $event.target.value;
    this.reloadData()
  }

  onChangeStatus($event: any)
  {
    this.status = $event.target.value;
    this.reloadData()
  }
}
