import { createReducer, on } from "@ngrx/store"
import { categoryChanged, statusChanged } from "./filter.actions"

export const filterFeatureKey = 'filter';

export interface FilterState
{
    category: string
    status: string
}

export const initialFilterState : FilterState = {
    category: 'all',
    status: 'all'
}

export const filterReducer = createReducer(
    initialFilterState,
    on(categoryChanged, (state, { category }) => ({...state, category: category })),
    on(statusChanged, (state, { status }) => ({...state, status: status }))
)