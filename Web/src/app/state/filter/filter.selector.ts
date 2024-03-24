import { createSelector } from "@ngrx/store";
import { AppState } from "../app.state";

export const selectFilterState = (state: AppState) => state.filter;

export const selectCategory = createSelector(
    selectFilterState,
    (state) => state.category
)

export const selectStatus = createSelector(
    selectFilterState,
    (state) => state.status
)