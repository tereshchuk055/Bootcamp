import React from "react";
import { Box } from '@mui/material';

import { TodoItem } from "./TodoItem";
import type { ITodoItem } from "../../types/type";

interface ITodoItemProperties {
    todoList: ITodoItem[]
}

export const TodoList: React.FC<ITodoItemProperties> = ({todoList}) => {
    return (
        <Box component="div" display="flex" flexDirection="column" >
            {todoList?.map((item) => (
                <TodoItem key={item.key} todoItem={item}></TodoItem>
            ))}
        </Box>
    )
}