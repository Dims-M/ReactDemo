class CommentBox extends React.Component {
    render() {
        return (
            <div className="commentBox">Здравствуй, мир! Я-окно комментариев.</div>
        );
    }
}

class CommentForm extends React.Component {
    render() {
        return (
            <div className="commentForm">Hello, world! I am a CommentForm.</div>
        );
    }

}

ReactDOM.render(<CommentBox />, document.getElementById('content'));