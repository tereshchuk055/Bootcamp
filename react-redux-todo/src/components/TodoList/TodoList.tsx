import React from "react";
import { Box, Paper, Button } from '@mui/material';

import { TodoItem } from "./TodoItem";
import { Todo } from "../../types/type";
import { useDispatch } from "react-redux"

interface TodoListProperties {
    todoList: Todo[],
    categories: Record<number, string>
}

export const TodoList: React.FC<TodoListProperties> = ({ todoList, categories }) => {
    const dispatch = useDispatch(); 
    const completedItems: Todo[] = todoList.filter(item => item.isCompleted);
    const uncompletedItems = todoList.filter(item => !item.isCompleted);
    return (
        <Box component="div" display="flex" flexDirection="column" sx={{ margin: 'auto' }}>
            {Object.entries(categories).map(([key, value]) => (
                <div key={key}>
                    <Paper sx={{ width: "720px", padding: '10px', margin: '3px', background: "white" }} elevation={1}>
                        <Box component="div" sx={{ width: "700px", margin: "3px 0", padding: "5px 0", fontSize: 25, fontWeight: "medium", color: "#3B3B3B", position:'relative'}}>
                            {value}
                            <Box component="span" onClick={() => { dispatch({ type: 'DELETE_CATEGORY', payload: Number(key) }) }} sx={{ fontSize: 12, position:'absolute', right:0, margin:1, cursor:'pointer' }}>&#10060;</Box>

                        </Box>
                        {uncompletedItems?.filter(item => item.categoryId == Number(key)).map((item) => (
                            <TodoItem key={item.id} todoItem={item}></TodoItem>
                        ))}
                        {completedItems?.filter(item => item.categoryId == Number(key)).map((item) => (
                            <TodoItem key={item.id} todoItem={item}></TodoItem>
                        ))}
                    </Paper>
                </div>
            ))}


        </Box>
    )
}