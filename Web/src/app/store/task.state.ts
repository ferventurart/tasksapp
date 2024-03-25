import { TaskModel } from "../models/task";

export const taskState : TaskModel = {
    list: [],
    errorMessage: '',
    editData: {
        id: '',
        description: '',
        dueDate: '',
        status: '',
        category: ''
    }
}