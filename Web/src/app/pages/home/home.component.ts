import { Component } from '@angular/core';
import { TableComponent } from '../../components/table/table.component';
import { FiltersComponent } from '../../components/filters/filters.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ TableComponent, FiltersComponent ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent{
  public state!: string;
  public category!: string;
}
