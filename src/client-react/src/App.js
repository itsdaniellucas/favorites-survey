import './App.css';
import React, { useEffect, useCallback } from 'react'
import { makeStyles } from '@material-ui/core/styles';
import { AppBar, Toolbar, Typography, LinearProgress } from '@material-ui/core'
import { teal, orange } from '@material-ui/core/colors';
import RouteConfig from './RouteConfig';
import AlertBox from './views/content/AlertBox'
import VoteDialog from './views/dialogs/VoteDialog'
import { useSelector, useDispatch } from 'react-redux'
import { selectIsFetching } from './store/globalSlice'
import signalRService from './services/signalRService'
import { selectQuestions, selectAnswers, getAnswers, getQuestions } from './store/resourceSlice'
import { selectStats, getStats, vote } from './store/surveySlice'
import { createMuiTheme, ThemeProvider } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
  appBar: {
    position: 'relative',
    color: teal[50],
  },
  title: {
    flexGrow: 1,
  },
  layout: {
    width: 'auto',
    padding: theme.spacing(4),
  },
  voteButton: {
    color: teal[50],
  },
}));

const theme = createMuiTheme({
  palette: {
    primary: {
      main: teal[800],
    },
    secondary: {
      main: orange[600],
    }
  }
})

function App() {
  const classes = useStyles();
  const isFetching = useSelector(selectIsFetching)
  const questions = useSelector(selectQuestions);
  const answers = useSelector(selectAnswers);
  const stats = useSelector(selectStats);
  const dispatch = useDispatch();

  const onSaveVote = useCallback((response) => {
    dispatch(vote(response));
  }, []);

  useEffect(() => {
    signalRService.initialize();

    if(questions.length == 0) {
      dispatch(getQuestions());
    }

    if(answers.length == 0) {
      dispatch(getAnswers());
    }

    if(stats.length == 0) {
      dispatch(getStats());
    }

  }, []);

  return (
    <div>
      <ThemeProvider theme={theme}>
        <AppBar className={classes.appBar} color="primary">
          <Toolbar>
            <Typography variant="h6" className={classes.title}>
              Favorites Survey
            </Typography>
            <VoteDialog className={classes.voteButton} onSave={onSaveVote} />
          </Toolbar>
        </AppBar>
        { isFetching ? <LinearProgress /> : null }
        <main className={classes.layout}>
          <RouteConfig />
          <AlertBox />
        </main>
      </ThemeProvider>
    </div>
  );
}

export default App;
