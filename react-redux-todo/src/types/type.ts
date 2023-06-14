import { format } from 'date-fns';
import { useSelector, TypedUseSelectorHook } from 'react-redux';
import { RootState } from "../redux/store";
import moment from 'moment';

export interface Todo {
    id: number,
    category: number
    name: string,
    deadline: string,
    checked: boolean
}

export interface Category {
    id: number,
    name: string
}

export interface TodoListState {
    todos: Todo[],
    categories: Category[]
}

export function serializeDate(date: Date): string {
    return format(date, 'dd.MM.yyyy HH:mm');
}

export function deserializeDate(serializedDate: string): Date {
    const date = moment(serializedDate, 'DD.MM.YYYY HH:mm').toDate();
    return date
}

export const useCurrentSelector: TypedUseSelectorHook<RootState> = useSelector;