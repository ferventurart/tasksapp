import { Component } from '@angular/core';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent {

  public status: string[] = ['Pending', 'Cancelled', 'Completed'];
  public categories: string[] = ['Orange', 'Yellow', 'Blue'];
  
  onChangeCategory(event: any)
  {

  }

  onChangeStatus(event: any)
  {
    
  }
}
