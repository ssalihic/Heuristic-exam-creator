'use strict';

customElements.define('compodoc-menu', class extends HTMLElement {
    constructor() {
        super();
        this.isNormalMode = this.getAttribute('mode') === 'normal';
    }

    connectedCallback() {
        this.render(this.isNormalMode);
    }

    render(isNormalMode) {
        let tp = lithtml.html(`<nav>
    <ul class="list">
        <li class="title">
            <a href="index.html" data-type="index-link">test-generator documentation</a>
        </li>
        <li class="divider"></li>
        ${ isNormalMode ? `<div id="book-search-input" role="search">
    <input type="text" placeholder="Type to search">
</div>
` : '' }
        <li class="chapter">
            <a data-type="chapter-link" href="index.html"><span class="icon ion-ios-home"></span>Getting started</a>
            <ul class="links">
                    <li class="link">
                        <a href="overview.html" data-type="chapter-link">
                            <span class="icon ion-ios-keypad"></span>Overview
                        </a>
                    </li>
                    <li class="link">
                        <a href="index.html" data-type="chapter-link">
                            <span class="icon ion-ios-paper"></span>README
                        </a>
                    </li>
                    <li class="link">
                        <a href="dependencies.html"
                            data-type="chapter-link">
                            <span class="icon ion-ios-list"></span>Dependencies
                        </a>
                    </li>
            </ul>
        </li>
        <li class="chapter modules">
            <a data-type="chapter-link" href="modules.html">
                <div class="menu-toggler linked" data-toggle="collapse"
                    ${ isNormalMode ? 'data-target="#modules-links"' : 'data-target="#xs-modules-links"' }>
                    <span class="icon ion-ios-archive"></span>
                    <span class="link-name">Modules</span>
                    <span class="icon ion-ios-arrow-down"></span>
                </div>
            </a>
            <ul class="links collapse"
            ${ isNormalMode ? 'id="modules-links"' : 'id="xs-modules-links"' }>
                    <li class="link">
                        <a href="modules/AppModule.html" data-type="entity-link">AppModule</a>
                            <li class="chapter inner">
                                <div class="simple menu-toggler" data-toggle="collapse"
                                    ${ isNormalMode ? 'data-target="#components-links-module-AppModule-6b2c9fa016cab1e74bde754c96dffd4d"' : 'data-target="#xs-components-links-module-AppModule-6b2c9fa016cab1e74bde754c96dffd4d"' }>
                                    <span class="icon ion-md-cog"></span>
                                    <span>Components</span>
                                    <span class="icon ion-ios-arrow-down"></span>
                                </div>
                                <ul class="links collapse"
                                    ${ isNormalMode ? 'id="components-links-module-AppModule-6b2c9fa016cab1e74bde754c96dffd4d"' : 'id="xs-components-links-module-AppModule-6b2c9fa016cab1e74bde754c96dffd4d"' }>
                                        <li class="link">
                                            <a href="components/AddQuestionComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">AddQuestionComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/AdminPanelComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">AdminPanelComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/AppComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">AppComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/EditQuestionComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">EditQuestionComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/FooterComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">FooterComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/GenerateTestComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">GenerateTestComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/HeaderComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">HeaderComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/HelloComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">HelloComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/LoginComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">LoginComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/PreviewQuestionComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">PreviewQuestionComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/PreviewTestsComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">PreviewTestsComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/RegisterComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">RegisterComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/SubjectComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">SubjectComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/SystemAdminPanelComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">SystemAdminPanelComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/TestPrintPreviewComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">TestPrintPreviewComponent</a>
                                        </li>
                                        <li class="link">
                                            <a href="components/VisibilityComponent.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules">VisibilityComponent</a>
                                        </li>
                                </ul>
                            </li>
                            <li class="chapter inner">
                                <div class="simple menu-toggler" data-toggle="collapse"
                                    ${ isNormalMode ? 'data-target="#injectables-links-module-AppModule-6b2c9fa016cab1e74bde754c96dffd4d"' : 'data-target="#xs-injectables-links-module-AppModule-6b2c9fa016cab1e74bde754c96dffd4d"' }>
                                    <span class="icon ion-md-arrow-round-down"></span>
                                    <span>Injectables</span>
                                    <span class="icon ion-ios-arrow-down"></span>
                                </div>
                                <ul class="links collapse"
                                    ${ isNormalMode ? 'id="injectables-links-module-AppModule-6b2c9fa016cab1e74bde754c96dffd4d"' : 'id="xs-injectables-links-module-AppModule-6b2c9fa016cab1e74bde754c96dffd4d"' }>
                                        <li class="link">
                                            <a href="injectables/AdminGuard.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>AdminGuard</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/ApiService.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>ApiService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/AuthGuard.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>AuthGuard</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/AuthService.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>AuthService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/AuthenticationService.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>AuthenticationService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/PublicGuard.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>PublicGuard</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/QuestionService.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>QuestionService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/StatusService.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>StatusService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/SubjectService.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>SubjectService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/SystemGuard.html" data-type="entity-link" data-context="sub-entity" data-context-id="modules"}>SystemGuard</a>
                                        </li>
                                </ul>
                            </li>
                    </li>
                    <li class="link">
                        <a href="modules/AppRoutingModule.html" data-type="entity-link">AppRoutingModule</a>
                    </li>
            </ul>
        </li>
        <li class="chapter">
            <div class="simple menu-toggler" data-toggle="collapse"
            ${ isNormalMode ? 'data-target="#classes-links"' : 'data-target="#xs-classes-links"' }>
                <span class="icon ion-ios-paper"></span>
                <span>Classes</span>
                <span class="icon ion-ios-arrow-down"></span>
            </div>
            <ul class="links collapse"
            ${ isNormalMode ? 'id="classes-links"' : 'id="xs-classes-links"' }>
                    <li class="link">
                        <a href="classes/Answer.html" data-type="entity-link">Answer</a>
                    </li>
                    <li class="link">
                        <a href="classes/Area.html" data-type="entity-link">Area</a>
                    </li>
                    <li class="link">
                        <a href="classes/DifficultyLevel.html" data-type="entity-link">DifficultyLevel</a>
                    </li>
                    <li class="link">
                        <a href="classes/Question.html" data-type="entity-link">Question</a>
                    </li>
                    <li class="link">
                        <a href="classes/QuestionType.html" data-type="entity-link">QuestionType</a>
                    </li>
                    <li class="link">
                        <a href="classes/Status.html" data-type="entity-link">Status</a>
                    </li>
                    <li class="link">
                        <a href="classes/Subject.html" data-type="entity-link">Subject</a>
                    </li>
                    <li class="link">
                        <a href="classes/Visibility.html" data-type="entity-link">Visibility</a>
                    </li>
                    <li class="link">
                        <a href="classes/YearOfStudy.html" data-type="entity-link">YearOfStudy</a>
                    </li>
            </ul>
        </li>
                <li class="chapter">
                    <div class="simple menu-toggler" data-toggle="collapse"
                        ${ isNormalMode ? 'data-target="#injectables-links"' : 'data-target="#xs-injectables-links"' }>
                        <span class="icon ion-md-arrow-round-down"></span>
                        <span>Injectables</span>
                        <span class="icon ion-ios-arrow-down"></span>
                    </div>
                    <ul class="links collapse"
                    ${ isNormalMode ? 'id="injectables-links"' : 'id="xs-injectables-links"' }>
                            <li class="link">
                                <a href="injectables/AdminGuard.html" data-type="entity-link">AdminGuard</a>
                            </li>
                            <li class="link">
                                <a href="injectables/ApiService.html" data-type="entity-link">ApiService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/AreaService.html" data-type="entity-link">AreaService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/AuthGuard.html" data-type="entity-link">AuthGuard</a>
                            </li>
                            <li class="link">
                                <a href="injectables/AuthService.html" data-type="entity-link">AuthService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/AuthenticationService.html" data-type="entity-link">AuthenticationService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/DifficultyLevelService.html" data-type="entity-link">DifficultyLevelService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/PublicGuard.html" data-type="entity-link">PublicGuard</a>
                            </li>
                            <li class="link">
                                <a href="injectables/QuestionService.html" data-type="entity-link">QuestionService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/QuestionTypeService.html" data-type="entity-link">QuestionTypeService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/StatusService.html" data-type="entity-link">StatusService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/SubjectService.html" data-type="entity-link">SubjectService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/SystemGuard.html" data-type="entity-link">SystemGuard</a>
                            </li>
                            <li class="link">
                                <a href="injectables/TestService.html" data-type="entity-link">TestService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/VisibilityService.html" data-type="entity-link">VisibilityService</a>
                            </li>
                            <li class="link">
                                <a href="injectables/YearOfStudyService.html" data-type="entity-link">YearOfStudyService</a>
                            </li>
                    </ul>
                </li>
        <li class="chapter">
            <div class="simple menu-toggler" data-toggle="collapse"
            ${ isNormalMode ? 'data-target="#interceptors-links"' : 'data-target="#xs-interceptors-links"' }>
                <span class="icon ion-ios-swap"></span>
                <span>Interceptors</span>
                <span class="icon ion-ios-arrow-down"></span>
            </div>
            <ul class="links collapse"
            ${ isNormalMode ? 'id="interceptors-links"' : 'id="xs-interceptors-links"' }>
                    <li class="link">
                        <a href="interceptors/TokenInterceptor.html" data-type="entity-link">TokenInterceptor</a>
                    </li>
            </ul>
        </li>
        <li class="chapter">
            <div class="simple menu-toggler" data-toggle="collapse"
            ${ isNormalMode ? 'data-target="#miscellaneous-links"' : 'data-target="#xs-miscellaneous-links"' }>
                <span class="icon ion-ios-cube"></span>
                <span>Miscellaneous</span>
                <span class="icon ion-ios-arrow-down"></span>
            </div>
            <ul class="links collapse"
            ${ isNormalMode ? 'id="miscellaneous-links"' : 'id="xs-miscellaneous-links"' }>
                    <li class="link">
                      <a href="miscellaneous/variables.html" data-type="entity-link">Variables</a>
                    </li>
            </ul>
        </li>
            <li class="chapter">
                <a data-type="chapter-link" href="routes.html"><span class="icon ion-ios-git-branch"></span>Routes</a>
            </li>
        <li class="chapter">
            <a data-type="chapter-link" href="coverage.html"><span class="icon ion-ios-stats"></span>Documentation coverage</a>
        </li>
        <li class="divider"></li>
        <li class="copyright">
                Documentation generated using <a href="https://compodoc.app/" target="_blank">
                            <img data-src="images/compodoc-vectorise.svg" class="img-responsive" data-type="compodoc-logo">
                </a>
        </li>
    </ul>
</nav>`);
        this.innerHTML = tp.strings;
    }
});
