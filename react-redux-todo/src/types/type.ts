import { format } from 'date-fns';
import { useSelector, TypedUseSelectorHook } from 'react-redux';
import type {RootState} from "../redux/store";
import moment from 'moment';

export interface ITodoItem {
    key: number,
    name: string,
    deadLine: string,
    checked: boolean
}

export interface TodoListState {
    items: ITodoItem[]
}

export function serializeDate(date: Date) : string{
    return format(date, 'dd.MM.yyyy HH:mm');
}

export function deserializeDate (serializedDate: string) : Date{
    const date = moment(serializedDate, 'DD.MM.YYYY HH:mm').toDate();
    return date
}

export const useCurrentSelector: TypedUseSelectorHook<RootState> = useSelector;