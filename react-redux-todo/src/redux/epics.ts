import { combineEpics, Epic, ofType } from "redux-observable";
import { PayloadAction } from "@reduxjs/toolkit";
import { catchError, map, mergeMap, Observable, of, concat } from "rxjs";
import { Category, Todo } from "../types/type";
import { GetTodosQuery, GetCategoriesQuery, AddTodoQuery, AddCategoryQuery, DeleteCategoryQuery, DeleteTodoQuery, ChangeCompletedStateQuery } from "../redux/queries";
import { setTodos, setCategories } from "./reducer"

const getTodosEpic: Epic = (action, state) => action.pipe(
    ofType("GET_TODOS"),
    mergeMap(() => GetTodosQuery(state.value).pipe(
        map((res: Todo[]) => setTodos(res)),
    ))
);

const getCategoriesEpic: Epic = (action, state) => action.pipe(
    ofType("GET_CATEGORIES"),
    mergeMap(() => concat(
        GetCategoriesQuery(state.value).pipe(
          mergeMap((res: Category[]) => of(setCategories(res)))
        ),
        of({ type: 'GET_TODOS' }) 
      )
    )
);

const addTodoEpic: Epic = (action: Observable<PayloadAction<Todo>>, state) => action.pipe(
    ofType("ADD_TODO"),
    mergeMap(action => AddTodoQuery(action.payload, state.value).pipe(
        map((res: boolean) => {
            if (res)
                return { type: "GET_CATEGORIES" };
        }),
    ))
);

const addCategoryEpic: Epic = (action: Observable<PayloadAction<Category>>, state) => action.pipe(
    ofType("ADD_CATEGORY"),
    mergeMap(action => AddCategoryQuery(action.payload, state.value).pipe(
        map((res: boolean) => {
            if (res)
                return { type: "GET_CATEGORIES" };
        }),
    ))
);

const deleteTodoEpic: Epic = (action: Observable<PayloadAction<number>>, state) => action.pipe(
    ofType("DELETE_TODO"),
    mergeMap(action => DeleteTodoQuery(action.payload, state.value).pipe(
        map((res: boolean) => {
            if (res)
                return { type: "GET_CATEGORIES" };
        }),
    ))
);

const deleteCategoryEpic: Epic = (action: Observable<PayloadAction<number>>, state) => action.pipe(
    ofType("DELETE_CATEGORY"),
    mergeMap(action => DeleteCategoryQuery(action.payload, state.value).pipe(
        map((res: boolean) => {
            if (res)
                return { type: "GET_CATEGORIES" };
        }),
    ))
);

const changeCompletedStateEpic: Epic = (action: Observable<PayloadAction<Todo>>, state) => action.pipe(
    ofType("CHANGE_COMPLETED_STATE"),
    mergeMap(action => ChangeCompletedStateQuery(action.payload as Todo, state.value).pipe(
        map((res: boolean) => {
            if (res)
                return { type: "GET_CATEGORIES" };
        }),
    ))
);

export const rootEpic: Epic = (action$, store$, dependencies) =>
    combineEpics(
        getTodosEpic,
        getCategoriesEpic,
        addTodoEpic,
        addCategoryEpic,
        deleteTodoEpic,
        deleteCategoryEpic,
        changeCompletedStateEpic
    )
        (action$, store$, dependencies).pipe(
            catchError((error, source) => {
                console.error(error);
                return source;
            })
        );