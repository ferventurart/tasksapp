import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AppState } from '../../state/app.state';
import { selectCategory, selectStatus } from '../../state/filter/filter.selector';
import { Store } from '@ngrx/store';
import { AsyncPipe } from '@angular/common';
import { categoryChanged, statusChanged } from '../../state/filter/filter.actions';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent {

  category$: Observable<string>
  status$: Observable<string>

  public status: string[] = ['Pending', 'Cancelled', 'Completed'];
  public categories: string[] = ['Orange', 'Yellow', 'Blue'];

  constructor(private store: Store<AppState>){
    this.category$ = this.store.select(selectCategory);
    this.status$ = this.store.select(selectStatus);
  }
  
  onChangeCategory($event: any)
  {
    this.store.dispatch(categoryChanged({ category:$event.target.value }))
    console.log($event.target.value);
  }

  onChangeStatus($event: any)
  {
    this.store.dispatch(statusChanged({ status: $event.target.value }))
    console.log($event.target.value);
  }
}
