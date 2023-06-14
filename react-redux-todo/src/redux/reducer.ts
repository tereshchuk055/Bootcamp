import { Category, Todo } from "../types/type"

import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState: { todos: Todo[], categories: Category[] } = {
    todos: [],
    categories: []
}


export const TodoItemSlice = createSlice({
    name: "todoList",
    initialState,
    reducers: {
        addTodo: (state, action: PayloadAction<Todo>) => {
            state.todos.push(action.payload);
        },
        addCategory: (state, action: PayloadAction<Category>) => {
            state.categories.push(action.payload);
        },
        changeCompletedState: (state, action: PayloadAction<number>) => {
            let item = state.todos.find(i => i.id === action.payload);
            if (item)
                item.checked = !item.checked;
        },
        deleteTodo: (state, action: PayloadAction<number>) => {
            state.todos = state.todos.filter(obj => obj.id !== action.payload);
        },
        deleteCategory: (state, action: PayloadAction<number>) => {
            state.categories = state.categories.filter(obj => obj.id !== action.payload);
            state.todos = state.todos.filter(obj => obj.category !== action.payload);
        }
    },
});

export const { addTodo, addCategory, changeCompletedState, deleteTodo, deleteCategory } = TodoItemSlice.actions;
export default TodoItemSlice.reducer;

