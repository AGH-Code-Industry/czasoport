# Workflow

## Git

### Branches

Pushes to `master` branch are protected. Each change, addition, hotfix etc. must be done on separate branch and then merged to `master` via pull request.

Branch names must follow this pattern: `type/short-description`. For example: `feature/adding-new-weapon`. Available types are:

 - `feature` - new addition, system, interaction, item, etc.
 - `bugfix` - fixing a known bug, error, etc.
 - `hotfix` - fixing a bug that is critical and needs to be fixed as soon as possible
 - `refactor` - refactoring code, changing code structure, etc.
 - `documentation` - adding new documentation, changing existing documentation, etc.
 - `spike` - trying out new idea, testing new system, etc.

`short-description` must be short, but descriptive. For example: `feature/adding-new-weapon` is good, but `feature/weapon` is not. Only *kebab-case* is allowed.

### Commits

There are no enforced rules on commit messages, but please use descriptive ones.

### Pull Requests

Please provide good name and at least short description of your pull request. If you are adding new feature, describe it as good as you can. If you are fixing a bug, describe what was the bug and how did you fix it.

Pull Requests must be approved by at least one **code owner**. **Code owners** are automatically added to each pull request.

**Merge commits** are disabled. Each pull request must be merged via **squash and merge**.

### Resolving conflicts

If you are resolving conflicts, please use **rebase** instead of **merge**. This will keep commit history clean. If you are not sure how to do it, please ask for help.

### Pair programming

If you are working on the same branch with someone else, please also use **rebase** instead of normal pull. 
