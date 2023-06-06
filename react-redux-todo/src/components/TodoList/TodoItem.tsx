import React from "react";
import { Box, Paper, Button } from '@mui/material';
import { ITodoItem } from "../../types/type";
import { useDispatch } from "react-redux"
import { deleteItem, changeCompletedState } from "../../redux/reducer";
import { deserializeDate } from "../../types/type";


interface ITodoItemProperties {
    todoItem: ITodoItem
}

export const TodoItem: React.FC<ITodoItemProperties> = ({todoItem}) => {
    const dispatch = useDispatch(); 

    return (
        <Paper sx={{ width: "700px", margin: "3px 0", background: todoItem.checked ? "#88fca7" : "white" }} elevation={4}>
            <Box component="div" display="flex" flexDirection="row" sx={{ width: "700px", margin: "3px 0", padding: "15px 0" }} onClick={() => { dispatch(changeCompletedState(todoItem.key)) }}>
                <Box component="span" display="flex" flexDirection="column" justifyContent="center" alignItems="flex-start"
                    sx={{ width: "50%", marginLeft: "30px" }}>
                    <Box component="span" sx={{ fontSize: 20, fontWeight: "medium", color: "##3B3B3B" }} >{todoItem.name}</Box>
                    <Box component="span" sx={{ fontSize: 14, color: deserializeDate(todoItem.deadLine) < new Date() && !todoItem.checked ? "red" : "grey" }}> {todoItem.deadLine}</Box>
                </Box>
                <Box component="span" display="flex" justifyContent="flex-end" alignItems="center" sx={{ width: "50%", marginRight: "30px" }} >
                    <Button variant={todoItem.checked ? "contained" : "outlined"} color="error" onClick={() => { dispatch(deleteItem(todoItem.key)) }} sx={{ margin: "0 5px", fontSize: 12 }}>Delete</Button>
                </Box>
            </Box>
        </Paper>
    )
}