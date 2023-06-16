export interface Todo {
    id: number,
    categoryId: number
    name: string,
    deadline: string,
    isCompleted: boolean
}

export interface Category {
    id: number,
    name: string
}

export interface TodoListState {
    todos: Todo[],
    categories: Category[],
    storage: string
}

