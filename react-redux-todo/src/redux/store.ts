import { configureStore } from '@reduxjs/toolkit'
import { createEpicMiddleware } from 'redux-observable';
import reducer from "./reducer";
import { rootEpic } from "./epics"

const epicMiddleware = createEpicMiddleware();
const store = configureStore({
    reducer: {
        reducer
    },
    middleware: [epicMiddleware],
});

epicMiddleware.run(rootEpic);

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>
export default store;