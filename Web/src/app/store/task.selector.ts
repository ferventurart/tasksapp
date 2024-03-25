import { createFeatureSelector, createSelector } from "@ngrx/store";
import { TaskModel } from "../models/task";

const getTaskState = createFeatureSelector<TaskModel>('tasks');

export const getTasksList = createSelector(getTaskState, (state) => {
    return state.list
})