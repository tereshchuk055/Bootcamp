import React from "react";
import { Box, Paper, Button } from '@mui/material';
import { Todo } from "../../types/type";
import { useDispatch } from "react-redux"
import { deserializeDate, serializeDate } from "../../helpers";


interface ITodoItemProperties {
    todoItem: Todo
}

export const TodoItem: React.FC<ITodoItemProperties> = ({todoItem}) => {
    const dispatch = useDispatch(); 

    return (
        <Paper sx={{ width: "700px", margin: "5px auto", background: todoItem.isCompleted ? "#88fca7" : "white" }} elevation={4}>
            <Box component="div" display="flex" flexDirection="row" sx={{ width: "700px", margin: "3px 0", padding: "15px 0" }} onClick={() => { dispatch({ type: 'CHANGE_COMPLETED_STATE', payload: todoItem }) }}>
                <Box component="span" display="flex" flexDirection="column" justifyContent="center" alignItems="flex-start"
                    sx={{ width: "50%", marginLeft: "30px" }}>
                    <Box component="span" sx={{ fontSize: 20, fontWeight: "medium", color: "#3B3B3B" }} >{todoItem.name}</Box>
                    <Box component="span" sx={{ fontSize: 14, color: deserializeDate(todoItem.deadline) < new Date() && !todoItem.isCompleted ? "red" : "grey" }}> {serializeDate(new Date(todoItem.deadline))}</Box>
                </Box>
                <Box component="span" display="flex" justifyContent="flex-end" alignItems="center" sx={{ width: "50%", marginRight: "30px" }} >
                    <Button variant={todoItem.isCompleted ? "contained" : "outlined"} color="error" onClick={() => { dispatch({ type: 'DELETE_TODO', payload: todoItem.id }) }} sx={{ margin: "0 5px", fontSize: 12 }}>Delete</Button>
                </Box>
            </Box>
        </Paper>
    )
}