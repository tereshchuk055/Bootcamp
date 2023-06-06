import { getActiveElement } from "@testing-library/user-event/dist/utils";
import { ITodoItem } from "../types/type"

import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState: { items: ITodoItem[] } = {
    items: []
}


export const TodoItemSlice = createSlice({
    name: "items",
    initialState,
    reducers: {
        addItem: (state, action: PayloadAction<ITodoItem>) => {
            state.items.push(action.payload);
        },
        changeCompletedState: (state, action: PayloadAction<number>) => {
            let item = state.items.find(i => i.key === action.payload);
            if (item)
                item.checked = !item.checked;
        },
        deleteItem: (state, action: PayloadAction<number>) => {
            state.items = state.items.filter(obj => obj.key !== action.payload);
        }
    },
});

export const { addItem, changeCompletedState, deleteItem } = TodoItemSlice.actions;
export default TodoItemSlice.reducer;

