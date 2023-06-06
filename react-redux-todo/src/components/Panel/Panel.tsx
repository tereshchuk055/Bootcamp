import React from 'react';
import { Box, TextField, Button } from '@mui/material';
import { DateTimePicker } from '@mui/x-date-pickers';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns'
import { ITodoItem, serializeDate } from '../../types/type'
import { useDispatch } from "react-redux"
import { addItem } from "../../redux/reducer";
import { useCurrentSelector } from '../../types/type';
import { GetNextId } from "../../helpers";


const DEFAULT = {name: '', deadLine: new Date()};

export const Panel = () => {
    const dispatch = useDispatch();
    const reduxState = useCurrentSelector(state => state);

    const [todoItem, setTodoItem] = React.useState(DEFAULT)

    const HandleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { value, name} = event.target;
        setTodoItem({...todoItem, [name]: value })
    }

    const HandleDateChange = (event: Date | null) => {
        setTodoItem({...todoItem, ['deadLine']: event ?? new Date() })
    }

    const HandleCLick = () => {
        let newItem :ITodoItem = {
            key : GetNextId(reduxState.reducer.items),
            name: todoItem.name,
            deadLine: serializeDate(todoItem.deadLine),
            checked: false
        }
        dispatch(addItem(newItem as ITodoItem))
        setTodoItem(DEFAULT);
    }

    return (
        <div>
            <Box sx={{ width: 700, borderRadius: 2}} component="div" display="flex" flexDirection="row" justifyContent="center">
                <TextField value={todoItem.name} onChange={HandleChange} label='To do:' sx={{ margin: 1 }} name="name" autoComplete='off'/>

                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DateTimePicker label="Deadline" sx={{ margin: 1 }} value={todoItem.deadLine} onChange={HandleDateChange} />
                </LocalizationProvider>
                
                <Button variant="contained" sx={{ margin: 1 }} onClick={HandleCLick}>Add Task</Button>
            </Box>
        </div>
    );
};
