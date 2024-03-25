import { createReducer, on } from '@ngrx/store';
import { taskState } from './task.state';
import { loadTasksFail, loadTasksSuccess } from './task.actions';

const _taskReducer = createReducer(
  taskState,
  on(loadTasksSuccess, (state, action) => {
    return {
      ...state,
      list: action.list,
      errorMessage: '',
      editData: {
        id: '',
        description: '',
        dueDate: '',
        status: '',
        category: '',
      },
    };
  }),
  on(loadTasksFail, (state, action) => {
    return {
      ...state,
      list: [],
      errorMessage: action.errorMessage,
    };
  })
);

export function TaskReducer(state: any, action: any) {
    return _taskReducer(state, action)
}