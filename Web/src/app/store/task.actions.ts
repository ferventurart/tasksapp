import { createAction, props } from "@ngrx/store"
import { Task } from "../models/task";

export const LOAD_TASKS = '[Task] Load Tasks';
export const LOAD_TASKS_SUCCESS = '[Task] Load Tasks Success';
export const LOAD_TASKS_FAIL = '[Task] Load Tasks Fail';

export const loadTasks = createAction(LOAD_TASKS, props<{ category: string, status: string }>())
export const loadTasksSuccess = createAction(LOAD_TASKS_SUCCESS, props<{ list: Task[] }>())
export const loadTasksFail = createAction(LOAD_TASKS_FAIL, props<{ errorMessage: string }>())