import { Component, Input, OnInit } from '@angular/core';
import { CategoryBadgeComponent } from '../category-badge/category-badge.component';
import { Task } from '../../models/task';
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [CategoryBadgeComponent],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent implements OnInit {

  @Input() state?: string;
  @Input() category?: string;
  public tasks!: Task[];
  
  constructor(private taskService: TaskService) {}


  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.taskService.getData(this.state ?? 'all', this.category ?? 'all').subscribe({
      next: (result) => {
        this.tasks = result;
      },
      error: (error) => console.error(error),
    });
  }
}
