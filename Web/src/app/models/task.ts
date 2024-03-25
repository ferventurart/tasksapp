export interface Task {
  id: string;
  description: string;
  status: string;
  dueDate: string | null;
  category: string;
}

export interface TaskModel {
  list: ReadonlyArray<Task>;
  errorMessage: string;
  editData: Task;
}
