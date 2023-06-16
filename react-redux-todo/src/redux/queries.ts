import { ajax } from 'rxjs/ajax';
import { map, catchError, of } from 'rxjs';
import { Category, Todo, TodoListState } from "../types/type";

export function GetTodosQuery(state: {reducer: TodoListState}) {
    interface GetTodosQueryStucture {
        data: {
            tasks: Todo[]
        }
    }

    return AjaxQuery<GetTodosQueryStucture>(state.reducer.storage, `{ tasks { id categoryId name deadline isCompleted } }`)
        .pipe(
            map(resp => {
                return resp.response.data.tasks
            }),
            catchError(error => {
                return of(error);
            })
        )
}

export function GetCategoriesQuery(state: {reducer: TodoListState}) {
    interface GetCategoriesQueryStucture {
        data: {
            categories: Category[]
        }
    }
    
    return AjaxQuery<GetCategoriesQueryStucture>(state.reducer.storage, `{ categories { id name } }`)
        .pipe(
            map(resp => {
                return resp.response.data.categories
            }),
            catchError(error => {
                return of(error);
            })
        )
}

export function AddTodoQuery(item: Todo, state: {reducer: TodoListState}) {
    interface CreateTodoQueryStucture {
        data: {
            createTask: string
        }
    }
    
    return AjaxQuery<CreateTodoQueryStucture>(state.reducer.storage, `mutation {createTask(task: { categoryId: ${item.categoryId}, name: "${item.name}", isCompleted: ${item.isCompleted}, deadline: "${item.deadline}"}) }`)
        .pipe(
            map(resp => {
                return JSON.parse(resp.response.data.createTask)
            }),
            catchError(error => {
                console.log(error);
                return of([])
            })
        )
}

export function AddCategoryQuery(item: Category, state: {reducer: TodoListState}) {
    interface CreateCategoryQueryStucture {
        data: {
            createCategory: string
        }
    }
    
    return AjaxQuery<CreateCategoryQueryStucture>(state.reducer.storage, `mutation { createCategory(category: { name: "${item.name}" }) }`)
        .pipe(
            map(resp => {
                return JSON.parse(resp.response.data.createCategory)
            }),
            catchError(error => {
                console.log(error);
                return of([])
            })
        )
}

export function DeleteTodoQuery(id: number, state: {reducer: TodoListState}) {
    interface DeleteTaskQueryStucture {
        data: {
            deleteTask: string
        }
    }
    
    return AjaxQuery<DeleteTaskQueryStucture>(state.reducer.storage, `mutation { deleteTask(taskId: ${id}) }`)
        .pipe(
            map(resp => {
                return JSON.parse(resp.response.data.deleteTask)
            }),
            catchError(error => {
                console.log(error);
                return of([])
            })
        )
}

export function DeleteCategoryQuery(id: number, state: {reducer: TodoListState}) {
    interface DeleteCategoryQueryStucture {
        data: {
            deleteCategory: string
        }
    }
    
    return AjaxQuery<DeleteCategoryQueryStucture>(state.reducer.storage, `mutation { deleteCategory(categoryId: ${id}) }`)
        .pipe(
            map(resp => {
                return JSON.parse(resp.response.data.deleteCategory)
            }),
            catchError(error => {
                console.log(error);
                return of([])
            })
        )
}

export function ChangeCompletedStateQuery(todo: Todo, state: {reducer: TodoListState}) {
    interface ChangeCompletedStateQueryStucture {
        data: {
            changeCompletedState: string
        }
    }

    return AjaxQuery<ChangeCompletedStateQueryStucture>(state.reducer.storage, `mutation { changeCompletedState(isCompleted: ${!todo.isCompleted}, taskId: ${todo.id}) }`)
        .pipe(
            map(resp => {
                return JSON.parse(resp.response.data.changeCompletedState)
            }),
            catchError(error => {
                console.log(error);
                return of([])
            })
        )
}

function AjaxQuery<T>(storage:string, query: string) {

    return ajax<T>({
        url: "https://localhost:7193/graphql",
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
            'Storage-Type': storage
        },
        body: JSON.stringify({
            query: query
        })
    })
}

