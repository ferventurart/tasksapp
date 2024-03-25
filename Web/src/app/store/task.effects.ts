import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TaskService } from '../services/task.service';
import { catchError, exhaustMap, map, of } from 'rxjs';
import { loadTasks, loadTasksFail, loadTasksSuccess } from './task.actions';

@Injectable()
export class TaskEffects {
  constructor(private action$: Actions, private service: TaskService) {}

  _loadTasks = createEffect(() =>
    this.action$.pipe(
      ofType(loadTasks),
      exhaustMap((action) => {
        return this.service.getData(action.status, action.category).pipe(
          map((data) => {
            return loadTasksSuccess({ list: data });
          }),
          catchError((_err) =>
            of(loadTasksFail({ errorMessage: _err.message }))
          )
        );
      })
    )
  );
}
