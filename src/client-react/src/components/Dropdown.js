import React, { useState, useEffect } from 'react';
import { FormControl, Select, MenuItem, InputLabel } from '@material-ui/core';

export default function Dropdown(props) {
    const [value, setValue] = useState(props.defaultValue || 1);

    const handleChange = (event) => {
        setValue(event.target.value);
        if(props.onDropdownChange) {
            props.onDropdownChange(event.target.value);
        }
    }

    useEffect(() => {
        setValue(props.defaultValue);
    }, [props.defaultValue]);

    return (
        <FormControl className={props.className}>
            <InputLabel>{props.label}</InputLabel>
            <Select value={value}
                    onChange={handleChange}>
                {props.data.map((x) => <MenuItem key={x.id} value={x.id}>{x.name}</MenuItem>)}
            </Select>
        </FormControl>
    )
}