export interface Task {
    id: string;
    description: string;
    status: string;
    dueDate: string | null;
    category: string;
  }