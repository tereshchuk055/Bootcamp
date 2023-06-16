import React from 'react';
import { Box, TextField, Button, Paper, InputLabel, MenuItem, FormControl } from '@mui/material';
import { DateTimePicker } from '@mui/x-date-pickers';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns'
import Select, { SelectChangeEvent } from '@mui/material/Select';
import { Category, Todo } from '../../types/type'
import { useDispatch } from "react-redux"
import { useCurrentSelector, GetNextId } from '../../helpers';
import { setStorage } from "../../redux/reducer";

interface PanelProperties {
    categories: Record<number, string>
}

export const Panel: React.FC<PanelProperties> = ({ categories }) => {

    let DEFAULT_TODO = { name: '', deadLine: new Date(), category: NaN };
    const DEFAULT_CATEGORY = { name: '' };

    const dispatch = useDispatch();
    const reduxState = useCurrentSelector(state => state);

    const [todo, setTodo] = React.useState(DEFAULT_TODO)
    const [category, setCategory] = React.useState(DEFAULT_CATEGORY)


    const HandleTodoChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { value, name } = event.target;
        setTodo({ ...todo, [name]: value })
    }

    const HandleCategoryChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { value, name } = event.target;
        setCategory({ ...category, [name]: value })
    }

    const HandleDateChange = (event: Date | null) => {
        setTodo({ ...todo, ['deadLine']: event ?? new Date() })
    }

    const HandleSelectChange = (event: SelectChangeEvent) => {
        setTodo({ ...todo, ['category']: Number(event.target.value) })
    };

    const HandleNewTodoCLick = () => {
        if (todo.name !== '' && todo.category != -1) {
            let newItem: Todo = {
                id: GetNextId(reduxState.reducer.todos),
                categoryId: todo.category,
                name: todo.name,
                deadline: todo.deadLine.toISOString(),
                isCompleted: false
            }
            dispatch({ type: 'ADD_TODO', payload: newItem })
            DEFAULT_TODO.category = newItem.categoryId;
            setTodo(DEFAULT_TODO);
        }
    }

    const HandleNewCategoryCLick = () => {
        let newCategory: Category = {
            id: GetNextId(reduxState.reducer.categories),
            name: category.name,
        }
        dispatch({ type: 'ADD_CATEGORY', payload: newCategory })
        setCategory(DEFAULT_CATEGORY);
    }

    const HandleChangeStorageCLick = () => {
        dispatch(setStorage(StorageType))
        dispatch({ type: 'GET_CATEGORIES' })
    }

    const StorageType = reduxState.reducer.storage == 'Sql' ? 'Xml' : 'Sql';

    return (
        <Paper sx={{ width: '90%', padding: "10px 0", background: "white", margin: '10px auto' }} elevation={4} >
            <Box component="div" display="flex" flexDirection="row" justifyContent="space-around">
                <Box sx={{ borderRadius: 2 }} component="div" display="flex" flexDirection="row" justifyContent="center">
                    <TextField value={category.name} onChange={HandleCategoryChange} label='Category:' sx={{ margin: 1 }} name="name" autoComplete='off' />
                    <Button variant="contained" sx={{ margin: 1 }} onClick={HandleNewCategoryCLick}>Add Category</Button>
                </Box>

                <Box sx={{ borderRadius: 2 }} component="div" display="flex" flexDirection="row" justifyContent="center">

                    <TextField value={todo.name} onChange={HandleTodoChange} label='To do:' sx={{ margin: 1 }} name="name" autoComplete='off' />
                    <Box sx={{ minWidth: 150, marginTop: 1 }}>
                        <FormControl fullWidth>
                            <InputLabel id="select-label">Category</InputLabel>
                            <Select labelId="select-label" label="Category" autoWidth onChange={HandleSelectChange} value={Number.isNaN(todo.category) ? undefined : todo.category.toString()}>
                                {Object.entries(categories).map(([key, value]) => (
                                    <MenuItem value={key} key={key}>{value}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Box>

                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                        <DateTimePicker label="Deadline" sx={{ margin: 1 }} value={todo.deadLine} onChange={HandleDateChange} />
                    </LocalizationProvider>

                    <Button variant="contained" sx={{ margin: 1 }} onClick={HandleNewTodoCLick}>Add Task</Button>
                </Box>

                <Box sx={{ borderRadius: 2 }} component="div" display="flex" flexDirection="row" justifyContent="center">
                    <Button variant="contained" sx={{ margin: 1 }} color='success' onClick={HandleChangeStorageCLick}>Change to {StorageType}</Button>
                </Box>
            </Box>
        </Paper>
    );
};
