import { Category, Todo } from "../types/type"
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState: { todos: Todo[], categories: Category[], storage: string } = {
    todos: [],
    categories: [],
    storage: 'Sql'
}


export const TodoItemSlice = createSlice({
    name: "todoList",
    initialState,
    reducers: {
        setTodos: (state, action: PayloadAction<Todo[]>) => {
            state.todos = action.payload;
        },
        setCategories: (state, action: PayloadAction<Category[]>) => {
            state.categories = action.payload;
        },
        setStorage: (state, action: PayloadAction<string>) => {
            state.storage = action.payload;
        },
    },
});

export const { setCategories, setTodos, setStorage } = TodoItemSlice.actions;
export default TodoItemSlice.reducer;

