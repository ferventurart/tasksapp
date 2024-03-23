import { createReducer, on } from '@ngrx/store';
import { changeCategory, changeStatus } from './filter.actions';

export const initialState = "all";

export const filterReducer = createReducer(
    initialState,
    on(changeCategory, (state) => state = state),
    on(changeStatus, (state) => state = state)
  );