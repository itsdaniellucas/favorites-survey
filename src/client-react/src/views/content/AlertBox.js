import React from 'react'
import { makeStyles } from '@material-ui/core/styles';
import { CircularProgress, Fade } from '@material-ui/core'
import { Alert } from '@material-ui/lab'
import { selectState } from '../../store/alertSlice'
import { useSelector } from 'react-redux'

const useStyles = makeStyles((theme) => ({
    progress: {
        marginRight: theme.spacing(0.5),
        marginBottom: theme.spacing(-0.5),
    },
}));

export default function AlertBox() {
    const classes = useStyles();
    const alertConfig = useSelector(selectState);

    return (
        <Fade in={alertConfig.visible}>
            {
                alertConfig.isProgress ?
                <Alert className="status-bar" icon={false} variant="filled" severity={alertConfig.severity}>
                    <CircularProgress className={classes.progress} size={18} color="inherit" />
                    {alertConfig.text}
                </Alert> :
                <Alert className="status-bar" variant="filled" severity={alertConfig.severity}>
                    {alertConfig.text}
                </Alert>
            }
        </Fade>
    )
}