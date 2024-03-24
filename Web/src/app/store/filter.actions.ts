import { createAction, props } from "@ngrx/store";

export const categoryChanged = createAction('[Filter Component] Category Changed', props<{ category: string; }>());
export const statusChanged = createAction('[Filter Component] Status Changed', props<{ status: string; }>());