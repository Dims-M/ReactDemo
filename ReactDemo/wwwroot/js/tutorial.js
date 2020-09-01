class CommentBox extends React.Component {
    render() {
        return (
            <div className="commentBox">Здравствуй, мир! Я-окно комментариев.</div>
        );
    }
}

ReactDOM.render(<CommentBox />, document.getElementById('content'));